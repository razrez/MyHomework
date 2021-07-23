class Sequense

  #init
  def initialize(n)
    @n=n
  end

  #n-th elem
  def find(n)
    @n=n
    s = '1'
    (n-1).times do |i|
      s = s.split('')
        .chunk_while { |a, b| a == b }
        .reduce('') { |s, el| "#{s}#{el.size}#{el.first}" }

    end
    return s
  end

  #print all sequense
  def print()

    s = '1'
    @n.times do |i|
      puts s

      s = s.split('')
        .chunk_while { |a, b| a == b }
        .reduce('') { |s, el| "#{s}#{el.size}#{el.first}" }
    end
  end

end

#creating objs
seq = Sequense.new(5)
seq.print()
puts seq.find(4)

#testing class
require 'test/unit'

class SequenseTest < Test::Unit::TestCase

  def test_find

    s = Sequense.new(10)

    assert_equal "1", s.find(1)
    assert_equal "11", s.find(2)
    assert_equal "21", s.find(3)

    assert_equal "1211", s.find(4)
    assert_equal "111221", s.find(5)
    assert_equal "312211", s.find(6)
    assert_equal "13112221", s.find(7)

    assert_equal "1113213211", s.find(8)
    assert_equal "31131211131221", s.find(9)
    assert_equal "13211311123113112211", s.find(10)

  end
end
